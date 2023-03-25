using AndonLights.Model;

namespace AndonLights.DTOs;

public record class AndonLightCreated(int id, LightStates state, string errorMessage, DateTime time);

