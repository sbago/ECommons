﻿using ECommons.UIHelpers.AtkReaderImplementations;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace ECommons.UIHelpers.AddonMasterImplementations;
public partial class AddonMaster
{
    public unsafe class LetterViewer : AddonMasterBase<AtkUnitBase>
    {
        public LetterViewer(nint addon) : base(addon) { }
        public LetterViewer(void* addon) : base(addon) { }

        public AtkComponentButton* TakeAllButton => Base->GetButtonNodeById(30);
        public AtkComponentButton* ReplyButton => Base->GetButtonNodeById(31);
        public AtkComponentButton* DeleteButton => Base->GetButtonNodeById(32);

        public void TakeAll() => ClickButtonIfEnabled(TakeAllButton);
        public void Reply() => ClickButtonIfEnabled(ReplyButton);
        public void Delete() => ClickButtonIfEnabled(DeleteButton);

        public Item[] Items
        {
            get
            {
                var reader = new ReaderLetterViewer(Base);
                var entries = new Item[reader.Items.Count];
                for(var i = 0; i < entries.Length; i++)
                    entries[i] = new (Base, reader.Items[i], i);
                return entries;
            }
        }

        public class Item(AtkUnitBase* Base, ReaderLetterViewer.Item handle, int index) : ReaderLetterViewer.Item(handle.AtkReaderParams.UnitBase, handle.AtkReaderParams.BeginOffset);
    }
}
