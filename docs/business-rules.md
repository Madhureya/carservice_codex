# Business Rules and Operational Design

## Service catalog
- Daily plans generate work orders every day.
- Weekly plans generate jobs twice weekly (Mon/Thu default in code).
- One-time plans trigger on start date.
- Car and bike pricing can be configured per plan.

## Capacity planning
- Assume ~1200 residents, target conversion 10% to 25% initially.
- Staff productivity baseline: 20-30 exterior jobs/day/person.
- Supervisor-to-staff ratio: 1:8 recommended.

## Key workflow
1. Customer registers vehicle.
2. Manager/Supervisor creates subscription on selected plan.
3. System generates daily work orders.
4. Supervisor assigns each order to staff.
5. Staff marks completed with notes/photo evidence.
6. Revenue and operational KPIs appear on dashboard.

## Missing-but-critical logic (planned)
- Skip/reschedule with reason codes (rain, vehicle unavailable).
- Replacement staff assignment SLA alerts.
- Complaint and quality audit workflow.
- Plan pause/holiday handling.
- Billing cycle reminders and failed-payment retry queue.
