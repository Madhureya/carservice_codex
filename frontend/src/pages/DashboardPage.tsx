import { useEffect, useState } from 'react';
import { DashboardMetrics } from '../types';
import { api } from '../services/api';

export function DashboardPage() {
  const [metrics, setMetrics] = useState<DashboardMetrics | null>(null);

  useEffect(() => {
    api.get('/analytics/dashboard').then((res) => setMetrics(res.data)).catch(() => setMetrics({
      activeCustomers: 0,
      activeSubscriptions: 0,
      pendingWorkOrders: 0,
      monthlyRevenue: 0
    }));
  }, []);

  if (!metrics) return <p>Loading dashboard...</p>;

  return (
    <section>
      <h1>Operations Dashboard</h1>
      <div className="cards">
        <article><h3>Active Customers</h3><p>{metrics.activeCustomers}</p></article>
        <article><h3>Active Subscriptions</h3><p>{metrics.activeSubscriptions}</p></article>
        <article><h3>Pending Work Orders</h3><p>{metrics.pendingWorkOrders}</p></article>
        <article><h3>Monthly Revenue</h3><p>₹ {metrics.monthlyRevenue}</p></article>
      </div>
    </section>
  );
}
