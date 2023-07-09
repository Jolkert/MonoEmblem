using MonoEmblem.Content;

namespace MonoEmblem.Data;
public interface IDeserializable<TSelf>
{
	static abstract TSelf FromJson(string id, ResourceManager resources);
}
