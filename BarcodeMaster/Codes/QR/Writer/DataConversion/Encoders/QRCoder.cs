using System;

namespace BarcodeMaster.Codes.QR.Writer.DataConversion.Encoders
{
    internal abstract class QRCoder
    {
        internal abstract int DataLength { get; }
        internal abstract bool TryEncoding(out byte[] binaryData);
    }
}
