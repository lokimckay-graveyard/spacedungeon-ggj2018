namespace VRTK.Examples
{
    using UnityEngine;

    public class GunBehaviour : VRTK_InteractableObject
    {
        public int damage = 1;
        public GameObject enemyHit;
        public GameObject bulletHole;
        public Transform muzzle;
        public float decalTime = 5f;

        public LayerMask castable;

        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);
            Shoot();
        }

        private void Shoot()
        {

            // Forward vector from Muzzle
            Vector3 fwd = muzzle.TransformDirection(Vector3.forward);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(muzzle.transform.position, fwd, 5000f, castable);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];

                if (hit.transform.tag == "Enemy")
                {
                    GameObject newDecal = Instantiate(enemyHit, hit.point, Quaternion.identity);
                    Destroy(newDecal, decalTime);
                    Enemy hitEnemyScript = hit.transform.GetComponent<Enemy>();
                    hitEnemyScript.ReceiveDamage(damage, gameObject.name);

                    Renderer rend = hitEnemyScript.MainMesh;
                    if (rend)
                    {
                        rend.material.color = Color.red;
                    }
                }

                // For everything else
                else
                {
                    GameObject newDecal = Instantiate(bulletHole, hit.point, Quaternion.identity);
                    Destroy(newDecal, decalTime);
                }
            }
        }
    }
}