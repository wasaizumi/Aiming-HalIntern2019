using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

class SoundImporter : AssetPostprocessor
{
    private void OnPreprocessAsset()
    {
        AudioImporter ai = assetImporter as AudioImporter;
        if(ai == null) return;

        AudioImporterSampleSettings set = new AudioImporterSampleSettings();

        ai.loadInBackground = false;
        ai.preloadAudioData = true;
        ai.ambisonic = false;
        set.loadType = AudioClipLoadType.CompressedInMemory;
        set.sampleRateOverride = 44100;
        set.compressionFormat = AudioCompressionFormat.Vorbis;

        if (ai.assetPath.Contains("BGM"))
        {
            set.quality = 0.8f;
            ai.forceToMono = false;
        }
        else
        {
            set.quality = 0.6f;
            ai.forceToMono = true;
        }
        ai.SetOverrideSampleSettings("PC", set);
        ai.SetOverrideSampleSettings("iOS", set);
        ai.SetOverrideSampleSettings("Android", set);
    }
}
