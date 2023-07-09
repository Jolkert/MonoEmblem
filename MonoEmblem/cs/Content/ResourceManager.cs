using Microsoft.Xna.Framework.Content;
using MonoEmblem.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MonoEmblem.Content;
public class ResourceManager
{
	private readonly ContentManager _assetContent;
	private readonly Dictionary<Type, string> _typeDirectories;
	private readonly Dictionary<string, object> _loadedData = new();

	public string AssetsRoot
	{
		get => _assetContent.RootDirectory;
		set => _assetContent.RootDirectory = value;
	}
	public string DataRoot = "";


	public ResourceManager(ContentManager assetContentManager, Dictionary<Type, string>? typeDirectories = null)
	{
		_assetContent = assetContentManager;
		_typeDirectories = typeDirectories ?? new Dictionary<Type, string>();
	}

	public void RegisterTypeDirectory<T>(string path) => RegisterTypeDirectory(typeof(T), path);
	public void RegisterTypeDirectory(Type type, string path) => _typeDirectories.Add(type, path);

	public T LoadAsset<T>(string assetName) => _assetContent.Load<T>(assetName);
	public T LoadJsonData<T>(string id)
	{
		string path = "";
		if (_typeDirectories.TryGetValue(typeof(T), out string? typeDir))
			path += $"{typeDir}/";
		path += $"{id}.json";
		return JsonConvert.DeserializeObject<T>(File.ReadAllText($"{DataRoot}/{path}"))!;
	}

	public T LoadDeserializable<T>(string id) where T : IDeserializable<T>
	{
		var key = $"{typeof(T)}:{id}";
		if (_loadedData.TryGetValue(key, out object? value) && value is T valueT)
			return valueT;

		T data = T.FromJson(id, this);
		_loadedData[key] = data;
		return data;
	}
	public bool RegisterDeserializable<T>(string id, T item) where T : IDeserializable<T> =>
		_loadedData.TryAdd($"{typeof(T)}:{id}", item);
}
