using AutoMapper;
using MedPoint.Data.Entities;
using MedPoint.Data.Models;
using NFluent;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.MedPoint.Data
{
    public class DtoMapperTests : BaseTest
    {
        private IMapper Mapper { get; }

        public DtoMapperTests()
        {
            Mapper = DefaultServiceProvider.GetService<IMapper>();
        }

        [Fact]
        public void Map_MapEntityToDto_FieldsHasCorrectData()
        {
            //Arrange
            var entity = new TestObject()
            {
                Id = Guid.NewGuid(),
                TestString = "Test String",
                User = new User()
                {
                    Id = Guid.NewGuid(),
                },
            };

            //Act
            var dto = Mapper.Map<TestObjectDto>(entity);

            //Assert
            Check.That(dto).IsNotNull();
            Check.That(dto.Id).IsEqualTo(entity.Id);
            Check.That(dto.TestString).IsEqualTo(entity.TestString);
            Check.That(dto.User).IsNotNull();
            Check.That(dto.User.Id).IsEqualTo(entity.User.Id);
        }

        [Fact]
        public void Map_MapDtoToEntity_NotMapIgnoredField()
        {
            //Arrange
            var dto = new TestObjectDto()
            {
                Id = Guid.NewGuid(),
                TestString = "Test String",
                User = new UserDto()
                {
                    Id = Guid.NewGuid(),
                },
            };

            //Act
            var entity = Mapper.Map<TestObject>(dto);

            //Assert
            Check.That(entity).IsNotNull();
            Check.That(entity.Id).IsEqualTo(dto.Id);
            Check.That(entity.TestString).IsEqualTo(dto.TestString);
            Check.That(entity.User).IsNull();
        }
    }
}
