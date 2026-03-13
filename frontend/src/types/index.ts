export type DashboardMetrics = {
  activeCustomers: number;
  activeSubscriptions: number;
  pendingWorkOrders: number;
  monthlyRevenue: number;
};

export type ServicePlan = {
  id: number;
  name: string;
  description: string;
  vehicleType: number;
  frequency: number;
  price: number;
  visitsPerCycle: number;
  includesInteriorCleaning: boolean;
  includesPolishing: boolean;
};

export type WorkOrder = {
  id: string;
  serviceDate: string;
  status: number;
  notes: string;
};
