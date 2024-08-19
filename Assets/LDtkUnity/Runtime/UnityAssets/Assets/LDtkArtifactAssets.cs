using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;

namespace LDtkUnity
{
    /// <summary>
    /// Stores the autogenerated sprites for level backgrounds.
    /// </summary>
    [HelpURL(LDtkHelpURL.SO_ARTIFACT_ASSETS)]
    public sealed class LDtkArtifactAssets : ScriptableObject
    {
        //I've debated if separate level background sprites should generate their own level background sprites.
        //but ive opted against it in favour of quick level reimports. This is okay because the project still has reference to the root information of levels.
        
        //I could consider making it where the level depends on and adds a dependency to the lvl background texture if separate level files are enabled.
        // The reason for this is that the sprite reference could be lost. however, the level depends on 
        //or otherwise, setup dependency from the project and store in there.
        //Overall, might be the safest option to just always build lvl background in the project. but separate level sounds great
        
        internal const string PROPERTY_BACKGROUNDS = nameof(_backgrounds);
        internal const string PROPERTY_DEFS = nameof(_definitions);

        [SerializeField] internal List<Sprite> _backgrounds = new List<Sprite>();
        [SerializeField] internal List<LDtkDefinitionObject> _definitions = new List<LDtkDefinitionObject>();
        
        private readonly Dictionary<string, Sprite> _indexedBackgrounds = new Dictionary<string, Sprite>();

        private bool _isIndexed;
        
#if UNITY_EDITOR
        private bool _willResetIndexed;
#endif

        /// <value>
        /// Gets all the background sprite assets used.
        /// </value>
        public List<Sprite> Backgrounds => _backgrounds;

        //todo the importer uses this for now for security reasons. might ease back into the indexed version if it's safe enough
        internal Sprite GetBackgroundSlow(string assetName)
        {
            return _backgrounds.FirstOrDefault(p => p.name == assetName);
        }
        
        /// <summary>
        /// Get a background by name from this import result.
        /// </summary>
        /// <param name="levelName">
        /// The name of the background asset.
        /// </param>
        /// <returns>
        /// The sprite that was generated in this import result.
        /// </returns>
        public Sprite GetBackgroundIndexed(string levelName)
        {
            if (!_isIndexed)
            {
                TrySetToReset();
                _isIndexed = true;

                foreach (var asset in _backgrounds)
                {
                    if (asset == null)
                    {
                        continue;
                    }
                
                    if (_indexedBackgrounds.ContainsKey(asset.name))
                    {
                        LDtkDebug.LogError("Tried instancing an asset an extra time. this should never happen, and the cached list should all be unique");
                        continue;
                    }
                
                    _indexedBackgrounds.Add(asset.name, asset);
                }
            }

            Profiler.BeginSample($"GetIndexedItem {nameof(Sprite)}");
            if (string.IsNullOrEmpty(levelName))
            {
                LDtkDebug.LogError("GetItem Tried getting an asset without a name");
                Profiler.EndSample();
                return null;
            }
            
            if (_indexedBackgrounds == null)
            {
                LDtkDebug.LogError($"GetItem The instanced dictionary was null when getting artifacts for {nameof(Sprite)} from \"{levelName}\"");
                Profiler.EndSample();
                return null;
            }
            
            if (_indexedBackgrounds.Count == 0)
            {
                LDtkDebug.LogError($"GetItem The instanced dictionary was empty! No values of {nameof(Sprite)} from \"{levelName}\"");
                Profiler.EndSample();
                return null;
            }

            if (_indexedBackgrounds.TryGetValue(levelName, out Sprite item))
            {
                Profiler.EndSample();
                return item;
            }

            Profiler.EndSample();
            return null;
        }

        public bool HasIndexedBackground(string assetName)
        {
            if (string.IsNullOrEmpty(assetName))
                return false;

            if (_indexedBackgrounds.IsNullOrEmpty())
            {
                return false;
            }

            return _indexedBackgrounds.ContainsKey(assetName);
        }

        private void TrySetToReset()
        {
#if UNITY_EDITOR
            if (_willResetIndexed)
            {
                return;
            }

            _willResetIndexed = true;
            UnityEditor.EditorApplication.delayCall += () =>
            {
                _willResetIndexed = false;
                _isIndexed = false;
                _indexedBackgrounds.Clear();
            };
#endif
        }
    }
}