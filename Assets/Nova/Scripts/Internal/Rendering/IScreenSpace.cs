// Copyright (c) Supernova Technologies LLC
namespace Nova.Internal.Rendering
{
    internal interface IScreenSpace
    {
        int CameraID { get; }
        void Update();
    }
}

