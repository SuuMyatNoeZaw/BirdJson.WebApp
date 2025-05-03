using Newtonsoft.Json;

namespace BirdJsonProject.EndPoint
{
    public static class BirdEndpoint
    {
        public static void MapEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/birds", () =>
            {
                string folderPath = "JsonData/Birds.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr);
                return Results.Ok(result.Tbl_Bird);
            })
.WithName("GetBirds")
.WithOpenApi();


            app.MapGet("/birds/{id}", (int id) =>
            {
                string folderPath = "JsonData/Birds.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr);
                var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
                if (item is null)
                {
                    return Results.BadRequest("No Data Found.");
                }
                return Results.Ok(item);
            })
            .WithName("GetBirdsByID")
            .WithOpenApi();

            app.MapPost("/birds", (BirdModel data) =>
            {
                string folderPath = "JsonData/Birds.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr);

                data.Id = result.Tbl_Bird.Count() == 0 ? 1 : result.Tbl_Bird.Max(x => x.Id) + 1;
                result.Tbl_Bird.Add(data);
                var jsonStr2 = JsonConvert.SerializeObject(result);
                File.WriteAllText(folderPath, jsonStr2);
                return Results.Ok(data);
            })
            .WithName("CreateBirds")
            .WithOpenApi();

            app.MapPut("/birds/{id}", (int id, BirdModel data) =>
            {
                string folderPath = "JsonData/Birds.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr);
                var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
                if (item is null)
                {
                    return Results.BadRequest("No Data Found.");
                }
                if (!string.IsNullOrEmpty(data.BirdMyanmarName))
                {
                    item.BirdMyanmarName = data.BirdMyanmarName;
                }
                if (!string.IsNullOrEmpty(data.BirdEnglishName))
                {
                    item.BirdEnglishName = data.BirdEnglishName;
                }
                if (!string.IsNullOrEmpty(data.Description))
                {
                    item.Description = data.Description;
                }
                if (!string.IsNullOrEmpty(data.ImagePath))
                {
                    item.ImagePath = data.ImagePath;
                }

                var jsonStr2 = JsonConvert.SerializeObject(result);
                File.WriteAllText(folderPath, jsonStr2);
                return Results.Ok(item);
            })
            .WithName("UpdateBirds")
            .WithOpenApi();

            app.MapDelete("/birds/{id}", (int id) =>
            {
                string folderPath = "JsonData/Birds.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr);
                var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
                if (item is null)
                {
                    return Results.BadRequest("No Data Found.");
                }
                result.Tbl_Bird.Remove(item);
                var jsonStr2 = JsonConvert.SerializeObject(result);
                File.WriteAllText(folderPath, jsonStr2);
                return Results.Ok();
            })
            .WithName("DeleteBird")
            .WithOpenApi();
        }
    }
}
