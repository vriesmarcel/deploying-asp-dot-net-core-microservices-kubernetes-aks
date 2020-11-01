using System.Collections.Generic;
using Microsoft.Extensions.Options;

using AutoMapper;

namespace AutoMapperBuilder
{
    public interface IAutoMapperBuilder
    {
        /// <summary>
        /// Represents a list <see cref="Profile"/> that gets added to the <see cref="MapperConfiguration"/> during <seealso cref="Build()"/>.
        /// </summary>
        public List<Profile> Profiles { get; }

        /// <summary>
        /// Builds AutoMapper with the configured <see cref="AutoMapperBuilderConfiguration"/>.
        /// </summary>
        /// <returns><seealso cref="IMapper"/>.</returns>
        IMapper Build();
    }

    /// <summary>
    /// The default implementation of <see cref="IAutoMapperBuilder"/>.
    /// </summary>
    public class AutoMapperBuilder : IAutoMapperBuilder
    {
        public List<Profile> Profiles { get; }

        public AutoMapperBuilder(IOptions<AutoMapperBuilderConfiguration> options)
        {
            Profiles = options.Value.Profiles;
        }

        public IMapper Build()
        {
            return new MapperConfiguration(c => c.AddProfiles(Profiles)).CreateMapper();
        }
    }
}