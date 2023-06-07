﻿using System.Collections.Generic;

namespace MyVinyl.com_authentication_service.Database.Converters
{
    // Useful: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/covariance-contravariance/creating-variant-generic-interfaces
    public interface IDtoConverter<Model, Request, Response>
    {
        Model DtoToModel(Request request);
        Response ModelToDto(Model model);
        List<Response> ModelToDto(List<Model> models);
    }
}
