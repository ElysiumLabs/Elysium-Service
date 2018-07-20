namespace Elysium.Data.Entities
{
    // CRUD :P
    public interface IDataEntityModel<TModel> :
        IDataEntityModelCreate<TModel>,
        IDataEntityModelRead<TModel>,
        IDataEntityModelUpdate<TModel>,
        IDataEntityModelDelete<TModel>
    {
        TModel ToModel(params object[] objs);
    }

    public interface IDataEntityModelCreate<TModel>
    {
        bool CreateFrom(TModel objParam);
    }

    public interface IDataEntityModelRead<TModel>
    {
        bool ReadFrom(TModel objParam);
    }

    public interface IDataEntityModelUpdate<TModel>
    {
        bool UpdateFrom(TModel objParam);
    }

    public interface IDataEntityModelDelete<TModel>
    {
        bool DeleteFrom(TModel objParam, bool softDelete = false);
    }
}