import { useEffect, useState } from 'react';
import { api } from '../services/api';
import { ServicePlan } from '../types';

export function PlansPage() {
  const [plans, setPlans] = useState<ServicePlan[]>([]);

  useEffect(() => {
    api.get('/plans').then((res) => setPlans(res.data));
  }, []);

  return (
    <section>
      <h1>Service Plans</h1>
      <table>
        <thead>
          <tr><th>Name</th><th>Frequency</th><th>Visits</th><th>Price</th><th>Interior</th><th>Polish</th></tr>
        </thead>
        <tbody>
          {plans.map((plan) => (
            <tr key={plan.id}>
              <td>{plan.name}</td>
              <td>{plan.frequency}</td>
              <td>{plan.visitsPerCycle}</td>
              <td>₹ {plan.price}</td>
              <td>{plan.includesInteriorCleaning ? 'Yes' : 'No'}</td>
              <td>{plan.includesPolishing ? 'Yes' : 'No'}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </section>
  );
}
