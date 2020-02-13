namespace Identity.API.Infrastructure
{
    using System.IO;
    using System.Reflection;
    using System.Security.Cryptography.X509Certificates;

    public static class Certificate
    {
        public static X509Certificate2 Get()
        {
            var assembly = typeof(Certificate).GetTypeInfo().Assembly;

            using var stream = assembly.GetManifestResourceStream("Identity.API.Certificate.idsrv3test.pfx");
;
            return new X509Certificate2(ReadStream(stream), "idsrv3test");
        }

        private static byte[] ReadStream(Stream stream)
        {
            var buffer = new byte[16 * 1024];

            using MemoryStream ms = new MemoryStream();

            int read;

            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                ms.Write(buffer, 0, read);

            return ms.ToArray();
        }
    }
}