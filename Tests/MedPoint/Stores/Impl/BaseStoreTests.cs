using MedPoint.Data.Entities;
using MedPoint.Data.Models;
using MedPoint.Stores;
using NFluent;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.MedPoint.Stores.Impl
{
    public class BaseStoreTests : BaseTest
    {
        private ITestObjectStore Store { get; }

        public BaseStoreTests()
        {
            Store = DefaultServiceProvider.GetService<ITestObjectStore>();
        }

        [Fact]
        public async Task Insert_Insert_ReturnDto()
        {
            //Arrange
            var dto = new TestObjectDto()
            {
                TestString = "test string",
            };

            //Act
            var response = await Store.Insert(dto);

            //Assert
            Check.That(response).IsNotNull();
            Check.That(response.TestString).IsEqualTo(dto.TestString);
        }

        [Fact]
        public async Task Insert_Insert_ObjectInDbContext()
        {
            //Arrange
            var dto = new TestObjectDto()
            {
                Id =  Guid.NewGuid(),
            };

            //Act
            await Store.Insert(dto);

            //Assert
            var dbObject = await DbContext.TestObjects.FirstOrDefaultAsync(o => o.Id == dto.Id);
            Check.That(dbObject).IsNotNull();
        }

        [Fact]
        public async Task InsertOrUpdate_Insert_ReturnDto()
        {
            //Arrange
            var dto = new TestObjectDto()
            {
                TestString = "test string",
            };

            //Act
            var response = await Store.InsertOrUpdate(dto);

            //Assert
            Check.That(response).IsNotNull();
            Check.That(response.TestString).IsEqualTo(dto.TestString);
        }

        [Fact]
        public async Task InsertOrUpdate_Insert_ObjectInBdContext()
        {
            //Arrange
            var dto = new TestObjectDto()
            {
                Id = Guid.NewGuid(),
            };

            //Act
            await Store.InsertOrUpdate(dto);

            //Assert
            var dbObject = await DbContext.TestObjects.FirstOrDefaultAsync(o => o.Id == dto.Id);
            Check.That(dbObject).IsNotNull();
        }

        [Fact]
        public async Task InsertOrUpdate_Update_ReturnUpdatedDto()
        {
            //Arrange
            var obj = new TestObject()
            {
                Id =  Guid.NewGuid(),
                TestString = "test string",
            };
            await InsertForTest(obj);

            var updatedObject = new TestObjectDto()
            {
                Id = obj.Id,
                TestString = "Updated string",
            };

            //Act
            var response = await Store.InsertOrUpdate(updatedObject);

            //Assert
            Check.That(response).IsNotNull();
            Check.That(response.Id).IsEqualTo(updatedObject.Id);
            Check.That(response.TestString).IsEqualTo(updatedObject.TestString);
        }

        [Fact]
        public async Task InsertOrUpdate_Update_UpdatedObjectInDbContext()
        {
            //Arrange
            var obj = new TestObject()
            {
                Id = Guid.NewGuid(),
                TestString = "test string",
            };
            await InsertForTest(obj);

            var updatedObject = new TestObjectDto()
            {
                Id = obj.Id,
                TestString = "Updated string",
            };

            //Act
            await Store.InsertOrUpdate(updatedObject);

            //Assert
            var dbObject = await DbContext.TestObjects.FirstOrDefaultAsync(o => o.Id == obj.Id);
            Check.That(dbObject).IsNotNull();
            Check.That(dbObject.TestString).IsEqualTo(updatedObject.TestString);
        }

        [Fact]
        public async Task GetBuId_Get_Entity()
        {
            //Arrange
            var entity = new TestObject
            {
                Id = Guid.NewGuid(),
                TestString = "test string",
            };
            await InsertForTest(entity);

            //Act
            var response = await Store.GetById(entity.Id);

            //Assert
            Check.That(response).IsNotNull();
            Check.That(response.TestString).IsEqualTo(entity.TestString);
        }

        [Fact]
        public async Task GetByIdIrDefault_GetNotExistEntity_ReturnDefault()
        {
            //Arrange
            Guid notExistId = Guid.NewGuid();

            //Act
            var response = await Store.GetByIdOrDefault(notExistId);

            //Assert
            Check.That(response).IsNull();
        }

        [Fact]
        public async Task Update_Update_UpdatedObjectInDbContext()
        {
            //Arrange
            var obj = new TestObject()
            {
                Id = Guid.NewGuid(),
                TestString = "test string",
            };
            await InsertForTest(obj);

            var updatedObject = new TestObjectDto()
            {
                Id = obj.Id,
                TestString = "Updated string",
            };

            //Act
            await Store.Update(updatedObject);

            //Assert
            var dbObject = await DbContext.TestObjects.FirstOrDefaultAsync(o => o.Id == obj.Id);
            Check.That(dbObject).IsNotNull();
            Check.That(dbObject.TestString).IsEqualTo(updatedObject.TestString);
        }

        [Fact]
        public async Task Update_Update_True()
        {
            //Arrange
            var obj = new TestObject()
            {
                Id = Guid.NewGuid(),
                TestString = "test string",
            };
            await InsertForTest(obj);

            var updatedObject = new TestObjectDto()
            {
                Id = obj.Id,
                TestString = "Updated string",
            };

            //Act
            var success = await Store.Update(updatedObject);

            //Assert
            Check.That(success).IsTrue();
        }

        [Fact]
        public async Task UpdateRange_UpdateRange_True()
        {
            //Arrange
            var obj1 = new TestObject()
            {
                Id = Guid.NewGuid(),
                TestString = "object 1",
            };
            var obj2 = new TestObject()
            {
                Id = Guid.NewGuid(),
                TestString = "object 2",
            };
            await InsertForTest(obj1, obj2);

            var updatedObject1 = new TestObjectDto()
            {
                Id = obj1.Id,
                TestString = "update object 1",
            };
            var updatedObject2 = new TestObjectDto()
            {
                Id = obj2.Id,
                TestString = "update object 2",
            };

            //Act
            var success = await Store.UpdateRange(new []{updatedObject1, updatedObject2});

            //Assert
            Check.That(success).IsTrue();
        }

        [Fact]
        public async Task Delete_Delete_DeleteObjectFromDbContext()
        {
            //Arrange
            var obj = new TestObject()
            {
                Id = Guid.NewGuid(),
                TestString = "object for delete",
            };
            await InsertForTest(obj);

            //Act
            var success = await Store.Delete(obj.Id);

            //Assert
            var dbObj = await DbContext.TestObjects.FirstOrDefaultAsync(o => o.Id == obj.Id);
            Check.That(success).IsTrue();
            Check.That(dbObj).IsNull();
        }
    }
}
