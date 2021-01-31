# Auto9Slicer
Auto 9 slice sprite generator on Unity.  
(The library used to be called [`OnionRing`](https://github.com/kyubuns/Auto9Slicer/tree/onionring))

![output](https://user-images.githubusercontent.com/961165/106372768-5c612480-63b6-11eb-9ff8-04394f6bb70b.gif)

<a href="https://www.buymeacoffee.com/kyubuns" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" height="41" width="174"></a>

## Simple Use

Create Auto9SliceTester. Assets > Create > Auto 9Slice > Tester

<img width="768" alt="Screen Shot 2021-01-31 at 11 25 46" src="https://user-images.githubusercontent.com/961165/106372836-135da000-63b7-11eb-85ad-d5fc9e6ee655.png">

Click `Run` to automatically slice the images in the same directory.

<img width="874" alt="Screen Shot 2021-01-31 at 11 27 20" src="https://user-images.githubusercontent.com/961165/106372854-4e5fd380-63b7-11eb-9b48-25105fc02edf.png">

## From Script

Add it to your EditorScript so that it will be executed at your desired timing.

```csharp
var slicedTexture = Auto9Slicer.Slicer.Slice(texture, SliceOptions.Default);
textureImporter.spriteBorder = slicedTexture.Border.ToVector4();
File.WriteAllBytes(filePath, slicedTexture.Texture.EncodeToPNG());
```

## Requirements

- Requires Unity2019.4 or later

## License

MIT License (see [LICENSE](LICENSE))
