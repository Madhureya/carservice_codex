# Car & Bike Cleaning Service Platform

This repository includes a starter **full-stack implementation** for your society-focused cleaning business using:
- **Backend:** ASP.NET Core 8 Web API + Entity Framework Core (SQL Server)
- **Frontend:** React + TypeScript (Vite)
- **Database:** Microsoft SQL Server schema script

## Project structure
- `backend/CarService.Api` - API for auth, plans, subscriptions, work orders, dashboards
- `frontend` - operations dashboard UI for managers/supervisors
- `sql/schema.sql` - relational schema and seed plans
- `docs/business-rules.md` - business logic and operating assumptions

## Suggested business roles
1. **EndUser:** customer owning vehicle and subscription
2. **Staff:** cleaner executing assigned work orders
3. **Supervisor:** assigns work to staff and tracks completion
4. **Manager:** analytics, payment visibility, escalations
5. **Admin:** user/role setup, global configuration

## Important business logic implemented
- Supports car and bike plans with daily, weekly, and one-time frequencies.
- Auto-generation of daily work orders for active subscriptions.
- Work order assignment and completion flow with photo placeholders.
- Subscription usage tracking via `RemainingVisits`.
- Basic manager dashboard metrics (active customers, pending orders, monthly revenue).

## Run backend (locally)
1. Install .NET SDK 8+
2. Update `backend/CarService.Api/appsettings.json` connection string and JWT key.
3. Apply schema from `sql/schema.sql` in your SQL Server.
4. Run API:
   ```bash
   cd backend/CarService.Api
   dotnet run
   ```

## Run frontend
```bash
cd frontend
npm install
npm run dev
```

## Next recommended upgrades
- Replace SHA256 password hash with ASP.NET Identity password hasher + salt.
- Add OTP/phone verification for society onboarding.
- Add customer app (mobile/web) for self-service booking and payments.
- Add route optimization for staff and attendance tracking.
- Integrate Razorpay/Stripe and invoice generation.
