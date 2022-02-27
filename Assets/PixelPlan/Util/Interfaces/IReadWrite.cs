namespace pixelplan {
    interface IReadJsonFile<T> {
        T ReadJsonFile(string filePath);
    }

    interface IWriteJsonFile<T> {
        void WriteJsonFile(T data);
    }

    interface IReadWriteFile<T> : IReadJsonFile<T>, IWriteJsonFile<T>
    { 
    }
}
