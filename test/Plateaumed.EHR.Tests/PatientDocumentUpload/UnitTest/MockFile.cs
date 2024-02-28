using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Plateaumed.EHR.Tests.PatientDocumentUpload.UnitTest;

public class MockFile: IFormFile
{
    private readonly MemoryStream _stream;

    /// <summary>
    /// Constructor for the MockFile
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="content"></param>
    /// <param name="length"></param>
    private MockFile(string fileName, string content = "dummy content",long length=0)
    {
        FileName = fileName;
        Length = length;
        _stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
    }

    public static MockFile CreateInstance(string fileName, string content = "dummy content", long length = 0)
    {
        return new MockFile(fileName, content,length);
    }

    public Stream OpenReadStream()
    {
        _stream.Position = 0; // Reset stream position to allow re-reading
        return _stream;
    }

    public void CopyTo(Stream target) { } 

    public Task CopyToAsync(Stream target, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.CompletedTask;
    }

    public string ContentType { get; }
    public string ContentDisposition { get; }
    public IHeaderDictionary Headers { get; }
    public long Length { get; }
    public string Name { get; }
    public string FileName { get; }
}
