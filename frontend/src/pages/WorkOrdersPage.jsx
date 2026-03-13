import { useEffect, useState } from 'react';
import { api } from '../services/api';

export function WorkOrdersPage() {
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    api.get('/workorders').then((res) => setOrders(res.data));
  }, []);

  return (
    <section>
      <h1>Today's Work Orders</h1>
      <ul>
        {orders.map((order) => (
          <li key={order.id}>
            {order.serviceDate} - Status #{order.status} - {order.notes || 'No notes yet'}
          </li>
        ))}
      </ul>
    </section>
  );
}
