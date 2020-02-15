using System.Collections;
using System.Collections.Generic;

namespace PalabrasAleatoriasOrigenDeDatos.Iterators
{
    public interface IQueryableLazyFetch<T> : IEnumerable<T>, IEnumerable
    {
    }
}
