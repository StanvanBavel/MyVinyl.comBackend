using MyVinyl.com.Database.Converters;
using MyVinyl.com.Database.Datamodels;
using MyVinyl.com.Database.Datamodels.Dtos;

using System.Collections.Generic;

namespace MyVinyl.com.Database.Converters
{
    public class VinylDtoConverter : IDtoConverter<Vinyl, VinylRequest, VinylResponse>
    {
        public Vinyl DtoToModel(VinylRequest request)
        {
            return new Vinyl
            {
                Name = request.Name,
                Description= request.Description,
                Image= request.Image
            };
        }

        public VinylResponse ModelToDto(Vinyl model)
        {
            return new VinylResponse
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Image = model.Image
            };
        }

        public List<VinylResponse> ModelToDto(List<Vinyl> models)
        {
            List<VinylResponse> responseDtos = new List<VinylResponse>();

            foreach (Vinyl vinyl in models)
            {
                responseDtos.Add(ModelToDto(vinyl));
            }

            return responseDtos;
        }
    }
}
