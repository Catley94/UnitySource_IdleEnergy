// Copyright (c) Supernova Technologies LLC
using Nova.Compat;
using Nova.Internal.Common;
using Nova.Internal.Core;
using Nova.Internal.Utilities;

namespace Nova.Internal.Rendering
{
    internal enum RenderRootType
    {
        Hierarchy,
        SortGroup,
    }

    internal struct RenderRootDataStore : IInitializable
    {
        public NovaHashMap<DataStoreID, RenderRootType> Roots;
        public NovaHashMap<DataStoreID, SortGroupInfo> SortGroupInfos;
        public NovaHashMap<DataStoreID, int> ScreenSpaceCameraTargets;

        public void AddHierarchyRoot(DataStoreID dataStoreID)
        {
            Roots[dataStoreID] = RenderRootType.Hierarchy;
        }

        public void RemoveHierarchyRoot(DataStoreID dataStoreID)
        {
            Roots.Remove(dataStoreID);
        }

        public void AddSortGroup(DataStoreID dataStoreID, ref SortGroupInfo sortGroupInfo)
        {
            Roots[dataStoreID] = RenderRootType.SortGroup;
            SortGroupInfos[dataStoreID] = sortGroupInfo;
        }

        public void RemoveSortGroup(DataStoreID dataStoreID)
        {
            Roots.Remove(dataStoreID);
            SortGroupInfos.Remove(dataStoreID);
        }

        public void Init()
        {
            Roots.Init(Constants.SomeElementsInitialCapacity);
            SortGroupInfos.Init(Constants.FewElementsInitialCapacity);
            ScreenSpaceCameraTargets.Init(Constants.FewElementsInitialCapacity);
        }

        public void Dispose()
        {
            Roots.Dispose();
            SortGroupInfos.Dispose();
            ScreenSpaceCameraTargets.Dispose();
        }
    }
}

