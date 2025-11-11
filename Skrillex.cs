using Godot;
using System;

using Godot;
using System.Threading.Tasks;

public partial class YourControlScript : Control
{
	[Export]
	private HBoxContainer objectContainer;

	[Export]
	private ScrollContainer scrollContainer;

	private float targetScroll = 0f;

	public override void _Ready()
	{
		_SetSelection();
	}

	private async void _SetSelection()
	{
		await ToSignal(GetTree().CreateTimer(0.01f), SceneTreeTimer.SignalName.Timeout);
		_SelectDeselectHighlight();
	}

	private async void _OnPreviousButtonPressed()
	{
		float scrollValue = targetScroll - _GetSpaceBetween();

		if (scrollValue < 0)
			scrollValue = _GetSpaceBetween() * 2;

		await _TweenScroll(scrollValue);
		_SelectDeselectHighlight();
	}

	private async void _OnNextButtonPressed()
	{
		float scrollValue = targetScroll + _GetSpaceBetween();

		if (scrollValue > _GetSpaceBetween() * 2)
			scrollValue = 0;

		await _TweenScroll(scrollValue);
		_SelectDeselectHighlight();
	}

	private float _GetSpaceBetween()
	{
		int distanceSize = objectContainer.GetThemeConstant("separation");
		var children = objectContainer.GetChildren();
		if (children.Count < 2) return 0;
		var objectSize = ((Control)children[1]).Size.X;
		return distanceSize + objectSize;
	}

	private async Task _TweenScroll(float scrollValue)
	{
		targetScroll = scrollValue;

		var tween = GetTree().CreateTween();
		tween.TweenProperty(scrollContainer, "scroll_horizontal", scrollValue, 0.25f);
		await ToSignal(tween, Tween.SignalName.Finished);
	}

	private void _SelectDeselectHighlight()
	{
		var selectedNode = GetSelectedValue();

		foreach (var child in objectContainer.GetChildren())
		{
			if (child is not TextureRect texture)
				continue;

			if (child == selectedNode)
				texture.Modulate = new Color(1, 1, 1);
			else
				texture.Modulate = new Color(0, 0, 0);
		}
	}

	private Node GetSelectedValue()
	{
		var selectionMarker = GetNode<Control>("%SelectionMarker");
		Vector2 selectedPosition = selectionMarker.GlobalPosition;

		foreach (var child in objectContainer.GetChildren())
		{
			if (child is Control control && control.GetGlobalRect().HasPoint(selectedPosition))
				return control;
		}

		return null;
	}
}
