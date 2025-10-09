using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Interface;
using Microsoft.AspNetCore.Http;
using Tesseract;
using ImageFormat = System.Drawing.Imaging.ImageFormat;

namespace Infraestructure.Service
{
    public class OCRService : IOCRService
    {
        private readonly string _tessdataPath;

        public OCRService()
        {
            // La ruta donde están los archivos de entrenamiento
            _tessdataPath = Path.Combine(Directory.GetCurrentDirectory(), "tessdata");

            // Verificar que existe la carpeta tessdata
            if (!Directory.Exists(_tessdataPath))
            {
                throw new DirectoryNotFoundException($"No se encontró la carpeta tessdata en: {_tessdataPath}");
            }
        }

        public async Task<string> ExtractTextFromCedulaAsync(IFormFile imageFile)
        {
            string tempFilePath = null;

            try
            {
                // 1. Guardar archivo temporalmente
                tempFilePath = Path.GetTempFileName();
                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // 2. Preprocesar la imagen (mejorar para OCR)
                var processedImagePath = PreprocessImage(tempFilePath);

                // 3. Usar Tesseract OCR
                using (var engine = new TesseractEngine(_tessdataPath, "spa", EngineMode.Default))
                {
                    // Configurar Tesseract para mejor reconocimiento
                    engine.SetVariable("tessedit_char_whitelist", "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ ");

                    using (var img = Pix.LoadFromFile(processedImagePath))
                    using (var page = engine.Process(img))
                    {
                        var extractedText = page.GetText();
                        var confidence = page.GetMeanConfidence();

                        Console.WriteLine($"Confianza del OCR: {confidence}");

                        // Limpiar texto
                        extractedText = CleanExtractedText(extractedText);

                        return extractedText;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en OCR: {ex.Message}");
            }
            finally
            {
                // Limpiar archivos temporales
                if (tempFilePath != null && File.Exists(tempFilePath))
                    File.Delete(tempFilePath);
            }
        }

        public async Task<bool> ValidateDocumentNumberAsync(IFormFile cedulaImage, string expectedDocumentNumber)
        {
            if (cedulaImage == null || cedulaImage.Length == 0)
                throw new Exception("La imagen de la cédula es requerida");

            // Validar tipo de archivo
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp" };
            var fileExtension = Path.GetExtension(cedulaImage.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
                throw new Exception("Solo se permiten imágenes JPG, JPEG, PNG o BMP");

            try
            {
                var extractedText = await ExtractTextFromCedulaAsync(cedulaImage);

                Console.WriteLine($"Texto extraído: {extractedText}");
                Console.WriteLine($"Documento esperado: {expectedDocumentNumber}");

                // Buscar el número de documento en el texto extraído
                var containsDocument = extractedText.Contains(expectedDocumentNumber);

                // También buscar sin espacios ni guiones
                var cleanExpected = expectedDocumentNumber.Replace(" ", "").Replace("-", "");
                var cleanExtracted = extractedText.Replace(" ", "").Replace("-", "");
                var containsCleanDocument = cleanExtracted.Contains(cleanExpected);

                return containsDocument || containsCleanDocument;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error validando cédula: {ex.Message}");
            }
        }

        private string PreprocessImage(string imagePath)
        {
            // Preprocesar imagen para mejorar OCR
            using (var originalImage = Image.FromFile(imagePath))
            using (var bitmap = new Bitmap(originalImage))
            {
                var processedPath = Path.GetTempFileName();

                // Convertir a escala de grises (mejora reconocimiento)
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    var attributes = new ImageAttributes();
                    attributes.SetColorMatrix(new ColorMatrix(new float[][]
                    {
                        new float[] {0.299f, 0.299f, 0.299f, 0, 0},
                        new float[] {0.587f, 0.587f, 0.587f, 0, 0},
                        new float[] {0.114f, 0.114f, 0.114f, 0, 0},
                        new float[] {0, 0, 0, 1, 0},
                        new float[] {0, 0, 0, 0, 1}
                    }));

                    graphics.DrawImage(bitmap,
                        new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                        0, 0, bitmap.Width, bitmap.Height,
                        GraphicsUnit.Pixel, attributes);
                }

                // Aumentar contraste
                var contrast = 1.5f;
                var brightness = 0.1f;

                for (int x = 0; x < bitmap.Width; x++)
                {
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        var pixel = bitmap.GetPixel(x, y);
                        var r = (int)Math.Min(255, (pixel.R - 128) * contrast + 128 + brightness * 255);
                        var g = (int)Math.Min(255, (pixel.G - 128) * contrast + 128 + brightness * 255);
                        var b = (int)Math.Min(255, (pixel.B - 128) * contrast + 128 + brightness * 255);

                        bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                bitmap.Save(processedPath, ImageFormat.Jpeg);
                return processedPath;
            }
        }

        private string CleanExtractedText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            // Limpiar y formatear texto extraído
            var lines = text.Split('\n')
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToArray();

            return string.Join("\n", lines);
        }
    }
}
