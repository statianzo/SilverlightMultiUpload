using System.IO;
namespace SilverlightMultiUploader.Framework
{
    public static class StreamExtensions
    {
        public static void WriteTo(this Stream source, Stream target )
        {
            WriteTo(source,target,1024);
        }
        public static void WriteTo(this Stream source, Stream target, int bufferLength)
        {
            var buffer = new byte[bufferLength];
            int bytesRead;
            while ((bytesRead = source.Read(buffer, 0, bufferLength)) > 0)
            {
                target.Write(buffer,0,bytesRead);
            }
        }
    }
}