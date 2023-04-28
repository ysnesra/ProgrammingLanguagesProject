using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core.CrossCuttingConcerns.Exceptions;

public class BusinessProblemDetails : ProblemDetails
{
    //İş hatası mı başka bir hata mı ayırmak için ayrı oluşturulmuş
    public override string ToString() => JsonConvert.SerializeObject(this);
}