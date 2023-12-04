using UnityEngine;

namespace Script
{
    public class CoordConvertor
    {
        // Taille de l'écran en pixels
        private static float screenWidth = 1920f;
        private static float screenHeight = 1080f;

        // Convertit les coordonnées normalisées (entre 0 et 1) en coordonnées du monde du jeu
        public static Vector2 Convert(float normalizedX, float normalizedY)
        {
            float worldX = (normalizedX - 0.5f) * screenWidth;
            float worldY = (0.5f - normalizedY) * screenHeight;
            return new Vector2(worldX, worldY);
        }
    }
}
