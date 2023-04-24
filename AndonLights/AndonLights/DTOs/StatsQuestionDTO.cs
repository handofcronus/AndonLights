using NodaTime;

namespace AndonLights.DTOs;

public record class StatsQuestionDTO(int id, ZonedDateTime Time);

