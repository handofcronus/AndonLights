using AndonLights.DTOs;
using AndonLights.Model;
using NodaTime;
using System.Runtime.CompilerServices;

namespace AndonLights.Mappers;

public static class AndonLightMapper
{
    public static AndonLight ToModel(this AndonLightDTO dto)
    {
        return new AndonLight(dto.Name)
        {
            Name = dto.Name,
            Id = dto.ID,
            CurrentState = dto.StateCode,
            DateOfCreation = new ZonedDateTime(Instant.FromDateTimeUtc(dto.DateofCreation), DateTimeZone.Utc),
            LastErrorMessage = dto.ErrorMessage?? "",
        };
    }

    public static List<AndonLight> ToModel(this List<AndonLightDTO> dtos)
    {
        var modelList = new List<AndonLight>();
        foreach (var dto in dtos)
        {
            modelList.Add(dto.ToModel());
        }
        return modelList;
    }

    public static List<AndonLightDTO> ToLightDTO(this List<AndonLight> lights)
    {
        var dtoList = new List<AndonLightDTO>();
        foreach (var light in lights)
        {
            dtoList.Add(light.ToLightDTO());
        }
        return dtoList;
    }



    public static AndonLightDTO ToLightDTO(this AndonLight light)
    {
        LocalDate dOC = light.DateOfCreation.Date;
        return new AndonLightDTO(light.Name)
        {
            ID = light.Id,
            StateCode = light.CurrentState,
            State = light.CurrentState.toString(),
            ErrorMessage = light.GetLastErrorMessage(),
            DateofCreation = new DateTime(dOC.Year, dOC.Month, dOC.Day)
        };
    }

    public static List<AndonStateDTO> ToStateDTO(this List<AndonLight> lights)
    {
        var dtoList = new List<AndonStateDTO>();
        foreach (var light in lights)
        {
            dtoList.Add(light.ToStateDTO());
        }
        return dtoList;
    }
    public static AndonStateDTO ToStateDTO(this AndonLight light)
    {
        return new AndonStateDTO(light.Id, light.CurrentState.toString())
        {
            ErrorMessage = light.States.Count == 0 ? "" : light.GetLastErrorMessage()
        };
    }
}