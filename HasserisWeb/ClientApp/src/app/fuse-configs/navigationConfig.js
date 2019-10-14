const navigationConfig = [
    {
        
        'id'      : 'applications',
        'title'   : 'Employees',
        'type'    : 'group',
        'icon'    : 'apps',
        'children': [
            {
                'id'   : 'calendar',
                'title': 'Calendar',
                'type' : 'item',
                'icon' : 'today',
                'url'  : '/apps/calendar'
            },
            {
                'id'      : 'dashboards',
                'title'   : 'Dashboards',
                'type'    : 'collapse',
                'icon'    : 'dashboard',
                'children': [
                    {
                        'id'   : 'analytics-dashboard',
                        'title': 'Employee Overview',
                        'type' : 'item',
                        'url'  : '/employee/overview'
                    },
                    {
                        'id'   : 'analytics-dashboard',
                        'title': 'Event Overview',
                        'type' : 'item',
                        'url'  : '/event/overview'
                    },
                    {
                        'id'   : 'analytics-dashboard',
                        'title': 'Customer Overview',
                        'type' : 'item',
                        'url'  : '/customer/overview'
                    }
                ]
            },
            {
                'id'      : 'e-commerce',
                'title'   : 'E-Commerce',
                'type'    : 'collapse',
                'icon'    : 'shopping_cart',
                'url'     : '/apps/e-commerce',
                'children': [
                    {
                        'id'   : 'e-commerce-products',
                        'title': 'Products',
                        'type' : 'item',
                        'url'  : '/apps/e-commerce/products',
                        'exact': true
                    },
                    {
                        'id'   : 'e-commerce-product-detail',
                        'title': 'Product Detail',
                        'type' : 'item',
                        'url'  : '/apps/e-commerce/products/1/a-walk-amongst-friends-canvas-print',
                        'exact': true
                    },
                    {
                        'id'   : 'e-commerce-new-product',
                        'title': 'New Product',
                        'type' : 'item',
                        'url'  : '/apps/e-commerce/products/new',
                        'exact': true
                    },
                    {
                        'id'   : 'e-commerce-orders',
                        'title': 'Orders',
                        'type' : 'item',
                        'url'  : '/apps/e-commerce/orders',
                        'exact': true
                    },
                    {
                        'id'   : 'e-commerce-order-detail',
                        'title': 'Order Detail',
                        'type' : 'item',
                        'url'  : '/apps/e-commerce/orders/1',
                        'exact': true
                    }
                ]
            }]}];
export default navigationConfig;
