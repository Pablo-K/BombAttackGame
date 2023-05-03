using BombAttackGame.Abstracts;
using BombAttackGame.Enums;
using BombAttackGame.Models;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace BombAttackGame.Global
{
    internal class InputHandler
    {

        public void HandleInputs(Player player) {

            var kstate = Keyboard.GetState();
            var mstate = Mouse.GetState();

            var mousePosition = mstate.Position.ToVector2();

            if (kstate.IsKeyDown(Keys.A)) { player.PlayerMove(Direction.Left); }
            if (kstate.IsKeyDown(Keys.S)) { player.PlayerMove(Direction.Down); }
            if (kstate.IsKeyDown(Keys.D)) { player.PlayerMove(Direction.Right); }
            if (kstate.IsKeyDown(Keys.W)) { player.PlayerMove(Direction.Up); }
            if (kstate.IsKeyDown(Keys.A) && kstate.IsKeyDown(Keys.W)) { player.PlayerMove(Direction.UpLeft); }
            if (kstate.IsKeyDown(Keys.A) && kstate.IsKeyDown(Keys.S)) { player.PlayerMove(Direction.DownLeft); }
            if (kstate.IsKeyDown(Keys.D) && kstate.IsKeyDown(Keys.W)) { player.PlayerMove(Direction.UpRight); }
            if (kstate.IsKeyDown(Keys.D) && kstate.IsKeyDown(Keys.S)) { player.PlayerMove(Direction.DownRight); }

            if (kstate.IsKeyDown(Keys.R)) {
                if(player.Inventory.SelectedItem is Gun gun)
                gun.AddReloadEvent(); 
            }

            if (kstate.IsKeyDown(Keys.D1)) {
                player.ChangeInventorySlot(1);
            }
            if (kstate.IsKeyDown(Keys.D2)) {
                player.ChangeInventorySlot(2);
            }
            if (kstate.IsKeyDown(Keys.D3)) {
                player.ChangeInventorySlot(3);
            }

            if (mstate.LeftButton == ButtonState.Pressed) { if (player.CanUseHoldableItem) player.UseSelectedItem(mousePosition); }

        }

    }
}
