using System.Collections.Generic;

using AutoMapper;

namespace AutoMapperBuilder
{
    /// <summary>
    /// Encapsulates a list of <see cref="Profile"/>s where custom <seealso cref="Profile"/>s can be added.
    /// </summary>
    public class AutoMapperBuilderConfiguration
    {
        /// <summary>
        /// Represents a list <see cref="Profile"/> that gets added to the <see cref="MapperConfiguration"/>.
        /// </summary>
        public List<Profile> Profiles { get; } = new List<Profile>();
    }
}