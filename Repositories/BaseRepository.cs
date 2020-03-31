using Notes.Models;
using Notes.Common.Enums;
using Notes.Data;
using Notes.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataResult<T>> Create<T>(T model) where T : BaseModel
        {
            using (var _transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var a = await _context.Set<T>().AddAsync(model);
                    var b = await _context.SaveChangesAsync();
                    _transaction.Commit();

                    return new DataResult<T>
                    {
                        Status = Status.Success,
                        Message = "Saved Successfully",
                        Data = model
                    };
                }
                catch (DbException ex)
                {
                    _transaction.Rollback();

                    return new DataResult<T>
                    {
                        Status = Status.Failed,
                        Message = $"Save failed, {ex.Message}",
                        Data = null
                    };
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();

                    return new DataResult<T>
                    {
                        Status = Status.Exception,
                        Message = $"Save failed, {ex.Message}",
                        Data = null                    };
                }
            }
        }

        public async Task<DataResult> CreateBatch<T>(List<T> model) where T : BaseModel
        {
            using (var _transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.Set<T>().AddRangeAsync(model);
                    await _context.SaveChangesAsync();
                    _transaction.Commit();

                    return new DataResult
                    {
                        Status = Status.Success,
                        Message = "Saved Successfully",
                        ReturnId = model.GetType().GetProperty("Id").ToString()
                    };
                }
                catch (DbException ex)
                {
                    _transaction.Rollback();

                    return new DataResult
                    {
                        Status = Status.Failed,
                        Message = $"Save failed, {ex.Message}"
                    };
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();

                    return new DataResult
                    {
                        Status = Status.Exception,
                        Message = $"Save failed, {ex.Message}"
                    };
                }
            }
        }

        public async Task<DataResult> Delete<T>(T model) where T : BaseModel
        {
            using (var _transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<T>().Remove(model);
                    await _context.SaveChangesAsync();
                    _transaction.Commit();

                    return new DataResult
                    {
                        Status = Status.Success,
                        Message = "Deleted Successfully",
                    };
                }
                catch (DbException ex)
                {
                    _transaction.Rollback();

                    return new DataResult
                    {
                        Status = Status.Failed,
                        Message = $"Delete failed, {ex.Message}"
                    };
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();

                    return new DataResult
                    {
                        Status = Status.Exception,
                        Message = $"Delete failed, {ex.Message}"
                    };
                }
            }
        }

        public async Task<DataResult> DeleteBatch<T>(List<T> model) where T : BaseModel
        {
            using (var _transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<T>().RemoveRange(model);
                    await _context.SaveChangesAsync();
                    _transaction.Commit();

                    return new DataResult
                    {
                        Status = Status.Success,
                        Message = "Deleted Successfully",
                    };
                }
                catch (DbException ex)
                {
                    _transaction.Rollback();

                    return new DataResult
                    {
                        Status = Status.Failed,
                        Message = $"Delete failed, {ex.Message}"
                    };
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();

                    return new DataResult
                    {
                        Status = Status.Exception,
                        Message = $"Delete failed, {ex.Message}"
                    };
                }
            }
        }

        public async Task<DataResult<T>> Update<T>(T model) where T : BaseModel
        {
            using (var _transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<T>().Update(model);
                    await _context.SaveChangesAsync();
                    _transaction.Commit();

                    return new DataResult<T>
                    {
                        Status = Status.Success,
                        Message = "Updated Successfully",
                        Data = model
                    };
                }
                catch (DbException ex)
                {
                    _transaction.Rollback();

                    return new DataResult<T>
                    {
                        Status = Status.Failed,
                        Message = $"Update failed, {ex.Message}"
                    };
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();

                    return new DataResult<T>
                    {
                        Status = Status.Exception,
                        Message = $"Update failed, {ex.Message}"
                    };
                }
            }
        }

        public async Task<DataResult> UpdateBatch<T>(List<T> model) where T : BaseModel
        {
            using (var _transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<T>().UpdateRange(model);
                    await _context.SaveChangesAsync();
                    _transaction.Commit();

                    return new DataResult
                    {
                        Status = Status.Success,
                        Message = "Updated Successfully",
                        ReturnId = model.GetType().GetProperty("Id").ToString()
                    };
                }
                catch (DbException ex)
                {
                    _transaction.Rollback();

                    return new DataResult
                    {
                        Status = Status.Failed,
                        Message = $"Update failed, {ex.Message}"
                    };
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();

                    return new DataResult
                    {
                        Status = Status.Exception,
                        Message = $"Update failed, {ex.Message}"
                    };
                }
            }
        }

        public IEnumerable<T> GetAll<T>() where T : BaseModel
        {
            return _context.Set<T>().OrderByDescending(x => x.CreatedDate);;
        }

        public IQueryable<T> GetAllAsync<T>() where T : BaseModel
        {
            return _context.Set<T>().OrderByDescending(x => x.CreatedDate);
        }

        public async Task<T> GetById<T, TKey>(TKey key) where T : BaseModel
        {
            return await _context.Set<T>().FindAsync(key);
        }

        public async Task<DataResult> DeleteUpdate<TDelete, TUpdate>(TDelete dModel, TUpdate uModel)
            where TDelete : BaseModel
            where TUpdate : BaseModel
        {
            using (var _transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<TDelete>().Remove(dModel);
                    _context.Set<TUpdate>().Update(uModel);
                    await _context.SaveChangesAsync();


                    _transaction.Commit();

                    return new DataResult
                    {
                        Status = Status.Success,
                        Message = "Updated Successfully",
                    };
                }
                catch (DbException ex)
                {
                    _transaction.Rollback();

                    return new DataResult
                    {
                        Status = Status.Failed,
                        Message = $"Update failed, {ex.Message}"
                    };
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();

                    return new DataResult
                    {
                        Status = Status.Exception,
                        Message = $"Update failed, {ex.Message}"
                    };
                }
            }
        }

        public async Task<DataResult> DeleteSave<TDelete, TSave>(TDelete dModel, TSave sModel)
            where TDelete : BaseModel
            where TSave : BaseModel
        {
            using (var _transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<TDelete>().Remove(dModel);
                    _context.Set<TSave>().Add(sModel);
                    await _context.SaveChangesAsync();


                    _transaction.Commit();

                    return new DataResult
                    {
                        Status = Status.Success,
                        Message = "Updated Successfully",
                        ReturnId = sModel.GetType().GetProperty("Id").ToString()
                    };
                }
                catch (DbException ex)
                {
                    _transaction.Rollback();

                    return new DataResult
                    {
                        Status = Status.Failed,
                        Message = $"Update failed, {ex.Message}"
                    };
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();

                    return new DataResult
                    {
                        Status = Status.Exception,
                        Message = $"Update failed, {ex.Message}"
                    };
                }
            }
        }

        public async Task<DataResult> DeleteBatchUpdate<TDelete, TUpdate>(List<TDelete> dModel, TUpdate uModel)
            where TDelete : BaseModel
            where TUpdate : BaseModel
        {
            using (var _transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<TDelete>().RemoveRange();
                    _context.Set<TUpdate>().Update(uModel);
                    await _context.SaveChangesAsync();


                    _transaction.Commit();

                    return new DataResult
                    {
                        Status = Status.Success,
                        Message = "Updated Successfully",
                        ReturnId = uModel.GetType().GetProperty("Id").ToString()
                    };
                }
                catch (DbException ex)
                {
                    _transaction.Rollback();

                    return new DataResult
                    {
                        Status = Status.Failed,
                        Message = $"Update failed, {ex.Message}"
                    };
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();

                    return new DataResult
                    {
                        Status = Status.Exception,
                        Message = $"Update failed, {ex.Message}"
                    };
                }
            }
        }

        public async Task<DataResult> DeleteBatchSave<TDelete, TSave>(List<TDelete> dModel, TSave sModel)
            where TDelete : BaseModel
            where TSave : BaseModel
        {
            using (var _transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<TDelete>().RemoveRange(dModel);
                    _context.Set<TSave>().Add(sModel);
                    await _context.SaveChangesAsync();


                    _transaction.Commit();

                    return new DataResult
                    {
                        Status = Status.Success,
                        Message = "Updated Successfully",
                        ReturnId = sModel.GetType().GetProperty("Id").ToString()
                    };
                }
                catch (DbException ex)
                {
                    _transaction.Rollback();

                    return new DataResult
                    {
                        Status = Status.Failed,
                        Message = $"Update failed, {ex.Message}"
                    };
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();

                    return new DataResult
                    {
                        Status = Status.Exception,
                        Message = $"Update failed, {ex.Message}"
                    };
                }
            }
        }
    }
}
