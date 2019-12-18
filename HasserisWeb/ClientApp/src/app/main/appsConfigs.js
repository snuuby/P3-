import {AnalyticsDashboardAppConfig} from './apps/dashboards/analytics/AnalyticsDashboardAppConfig';
import {ProjectDashboardAppConfig} from './apps/dashboards/project/ProjectDashboardAppConfig';
import {MailAppConfig} from './apps/mail/MailAppConfig';
import {TodoAppConfig} from './apps/todo/TodoAppConfig';
import {ContactsAppConfig} from './apps/contacts/ContactsAppConfig';
import {FileManagerAppConfig} from './apps/file-manager/FileManagerAppConfig';
import {CalendarAppConfig} from './calendar/CalendarAppConfig';
import {ChatAppConfig} from "./apps/chat/ChatAppConfig";
import {ECommerceAppConfig} from './apps/e-commerce/ECommerceAppConfig';
import {ScrumboardAppConfig} from './apps/scrumboard/ScrumboardAppConfig';
import {AcademyAppConfig} from './apps/academy/AcademyAppConfig';
import { NotesAppConfig } from './apps/notes/NotesAppConfig';
import { LoginConfig } from "./login/LoginConfig";
import { ProfilePageConfig } from "./profile/ProfilePageConfig";
import { EmployeeOverviewConfig } from "./employee/EmployeeOverviewConfig";
import { ToolOverviewConfig } from "./tool/ToolOverviewConfig";
import { VehicleOverviewConfig } from "./vehicle/VehicleOverviewConfig";
import { CustomerOverviewConfig } from "./customer/CustomerOverviewConfig";
import { InspectionsOverviewConfig } from "./tasks/inspections/InspectionsOverviewConfig";
import { OffersOverviewConfig } from "./tasks/offers/OffersOverviewConfig";
import { TaskOverviewConfig } from "./tasks/task/TaskOverviewConfig";
import { CreateTaskConfig} from "./tasks/task/CreateTaskConfig";
import { CreateOfferConfig} from "./tasks/offers/CreateOfferConfig";
import { CreateInspectionReportConfig} from "./tasks/inspections/CreateInspectionReportConfig";


export const appsConfigs = [
    AnalyticsDashboardAppConfig,
    ProjectDashboardAppConfig,
    MailAppConfig,
    TodoAppConfig,
    FileManagerAppConfig,
    ContactsAppConfig,
    CalendarAppConfig,
    ChatAppConfig,
    ECommerceAppConfig,
    ScrumboardAppConfig,
    AcademyAppConfig,
    NotesAppConfig,
    EmployeeOverviewConfig,
    LoginConfig,
    ProfilePageConfig,
    ToolOverviewConfig,
    VehicleOverviewConfig,
    CustomerOverviewConfig,
    InspectionsOverviewConfig,
    OffersOverviewConfig,
    TaskOverviewConfig,
    CreateTaskConfig,
    CreateOfferConfig,
    CreateInspectionReportConfig
];
