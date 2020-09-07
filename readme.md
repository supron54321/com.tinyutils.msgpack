# Tiny message pack library for unity

Repository contains small and fast message pack serialization library, for Unity DOTS runtime.

This library provides only low level writers and readers. If you are interested in high level code generation, check [JsInterop](https://github.com/supron54321/com.tinyutils.jsinterop/) repository.

# Usage

## Reader

```C#
public int[] ReadIntArray(NativeArray<byte> buffer)
{
    MsgPackReader reader = new MsgPackReader(buffer);
    if(reader.ReadArrayHeader(out var arrayLength))
    {
        int[] array = new int[arrayLength];
        for(int i = 0; i < array.Length; i++)
        {
            array[i] = reader.ReadInt32();
        }
        return array;
    }
    return null;
}
```

## Writer

```C#
public NativeArray<byte> WriteIntArray(int[] array, Allocator allocator)
{
    using(MsgPackWriter writer = new MsgPackWriter(Allocator.Temp))
    {
        if(array != null)
        {
            writer.WriteArrayHeader(array.Length);
            foreach(var value in array)
                writer.WriteInteger(value);
        }
        else{
            writer.WriteNil();
        }

        return writer.ToArray(allocator);
    }
}
```
