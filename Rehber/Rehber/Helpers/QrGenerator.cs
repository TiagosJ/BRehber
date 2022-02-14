using QRCoder;
using System.Drawing;
using System.IO;

namespace Rehber.Helpers
{
    public static class QrGenerator
    {
        public static byte[] generateQrCode(string qrText)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            byte[] returnValue;

            using (var stream = new MemoryStream())
            {
                qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                returnValue = stream.ToArray();
            }

            return returnValue;
        }
    }
}
