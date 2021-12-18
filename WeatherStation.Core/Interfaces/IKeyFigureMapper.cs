using System.Collections.Generic;
using WeatherStation.Model.KeyFigure;

namespace WeatherStation.Core.Interfaces
{
    public interface IKeyFigureMapper<T> where T : class
    {
        KeyFigure MapTo(T item);
        IEnumerable<KeyFigure> MapToMany(T item);
        T MapFrom(KeyFigure keyFigure);
    }
}
