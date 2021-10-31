using System.Collections.Generic;

namespace Model.InMemoryDataAccess
{
    /// <summary>
    /// LocationStore is an in-memory store of Location data.
    /// For production, a database would be used instead.
    /// </summary>
    public class LocationStore: List<Location>
    {
    }
}
