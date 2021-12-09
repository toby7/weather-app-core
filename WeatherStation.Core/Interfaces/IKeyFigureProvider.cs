using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherStation.Core.Interfaces
{
    using Model.KeyFigure;

    public interface IKeyFigureProvider
    {
        string Name { get; }

        Task<KeyFigure> Get();

        Task<IEnumerable<KeyFigure>> Get(DateTimeOffset? fromDate = null);
    }
}
