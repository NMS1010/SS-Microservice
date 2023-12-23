import { ConfigProvider, notification } from "antd";

notification.config({
  placement: "bottomRight"
});

export default function AntdConfigProvider({ children }) {
  return (
    <ConfigProvider
    >
      {children}
    </ConfigProvider>
  );
}