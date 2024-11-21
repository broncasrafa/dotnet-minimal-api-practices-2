using Student.Domain.Exceptions.Common;

namespace Student.Domain.Extensions;

public static class ObjectExtensions
{
    public static T OrElseThrows<T>(this T entity, BaseException exception)
    {
        if (entity is null) throw exception;
        return entity;
    }

    public static async Task<T> OrElseThrowsAsync<T>(this Task<T> task,  BaseException exception)
    {
        var entity = await task;
        if (entity is null) throw exception;
        return entity;
    }
}
