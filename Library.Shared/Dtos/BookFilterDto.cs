using Library.Shared.Enums;
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
     string? OriginallyPublished,
     string? Author,
     int? PagesCount,
     string? Illustrator,
     string? Genre,
     EOrder? OrderBy
    );
}