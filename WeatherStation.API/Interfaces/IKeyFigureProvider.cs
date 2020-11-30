using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherStation.API.Interfaces
{
    using Controllers;
    using Model.KeyFigure;

    public interface IKeyFigureProvider
    {
        string Name { get; }

        Task<KeyFigure> Get();

        Task<IEnumerable<KeyFigure>> Get(DateTimeOffset? fromDate = null);
    }
}
