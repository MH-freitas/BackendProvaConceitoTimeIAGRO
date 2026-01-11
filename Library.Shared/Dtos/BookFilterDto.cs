using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Librery.Shared.Dtos
{
    public record BookFilterDto
    (
     int? Id,
     string? Name,
     double? Price,
     DateTime OriginallyPublished,
     string? Author,
     int? PagesCount,
     string? Ilustrator,
     string[]? Genres
    );
}