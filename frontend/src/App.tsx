import { Link, Route, Routes } from 'react-router-dom';
import { DashboardPage } from './pages/DashboardPage';
import { PlansPage } from './pages/PlansPage';
import { WorkOrdersPage } from './pages/WorkOrdersPage';

export function App() {
  return (
    <div className="layout">
      <aside>
        <h2>CarService</h2>
        <nav>
          <Link to="/">Dashboard</Link>
          <Link to="/plans">Plans</Link>
          <Link to="/work-orders">Work Orders</Link>
        </nav>
      </aside>
      <main>
        <Routes>
          <Route path="/" element={<DashboardPage />} />
          <Route path="/plans" element={<PlansPage />} />
          <Route path="/work-orders" element={<WorkOrdersPage />} />
        </Routes>
      </main>
    </div>
  );
}
