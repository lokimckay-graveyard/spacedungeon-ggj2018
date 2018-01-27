namespace VRTK.Examples
{
    using UnityEngine;

    public class GunBehaviour : VRTK_InteractableObject
    {
        public GameObject enemyHit;
        public GameObject bulletHole;
        public Transform muzzle;

        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);
            Shoot();
        }

        private void Shoot()
        {
            RaycastHit hit;

            // Forward vector from Muzzle
            Vector3 fwd = muzzle.TransformDirection(Vector3.forward);

            // Raycast forward from Muzzle, max 25 units
            if (Physics.Raycast(muzzle.transform.position, fwd, out hit, 125))
            {

                // Gunshot sound & animation goes here


                ///////////////////////////////////////


                // If you hit an enemy -> Do something
                if (hit.transform.tag == "enemy")
                {
                    Instantiate(enemyHit, hit.point, Quaternion.identity);
                }

                // For everything else
                else
                {
                    Instantiate(bulletHole, hit.point, Quaternion.identity);
                }


            }
        }
    }
}