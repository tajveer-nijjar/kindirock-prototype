using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace GridLayoutDemo
{
    public class RecyclerAdapter : RecyclerView.Adapter
    {
        public event EventHandler<RecyclerAdapterClickEventArgs> ItemClick;
        public event EventHandler<RecyclerAdapterClickEventArgs> ItemLongClick;
        List<int> _items;

        public RecyclerAdapter(List<int> data)
        {
            _items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.single_card, parent, false);

            var vh = new RecyclerAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var holder = viewHolder as RecyclerAdapterViewHolder;
            holder.Caption.Text = $"Cell {_items[position].ToString()}";
        }

        public override int ItemCount => _items.Count;

        void OnClick(RecyclerAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(RecyclerAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class RecyclerAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView Caption { get; set; }

        public RecyclerAdapterViewHolder(View itemView, Action<RecyclerAdapterClickEventArgs> clickListener,
                            Action<RecyclerAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            Caption = (TextView)itemView.FindViewById(Resource.Id.textView);
            itemView.Click += (sender, e) => clickListener(new RecyclerAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new RecyclerAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class RecyclerAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}