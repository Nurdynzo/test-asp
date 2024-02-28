using Abp.Dependency;

namespace Plateaumed.EHR.Web.Xss
{
    public interface IHtmlSanitizer: ITransientDependency
    {
        string Sanitize(string html);
    }
}