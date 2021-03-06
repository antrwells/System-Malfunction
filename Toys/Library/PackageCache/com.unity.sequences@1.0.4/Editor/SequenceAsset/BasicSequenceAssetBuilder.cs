using UnityEngine;
using UnityEngine.Sequences;

namespace UnityEditor.Sequences
{
    internal class BasicSequenceAssetBuilder : ISequenceAssetBuilder
    {
        public GameObject CreateSequenceAsset(string type)
        {
            return SequenceAssetUtility.CreateSource($"{type}Asset", type);
        }

        public ISequenceAssetView CreateSequenceAssetView()
        {
            return new BasicSequenceAssetView();
        }
    }
}
