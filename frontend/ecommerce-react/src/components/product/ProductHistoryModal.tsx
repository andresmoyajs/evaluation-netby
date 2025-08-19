import { useQuery } from "@tanstack/react-query";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
} from "@/components/ui/dialog";
import { CheckCircle, ArrowUp, ArrowDown, RefreshCcw } from "lucide-react"; // iconos
import type { Product } from "@/types/product";
import type { Transaction } from "@/types/transaction";
import { getProductTransactions } from "@/actions/transactionsAction";

export default function ProductHistoryModal({
  product,
  setOpen,
  open,
}: {
  product: Product;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
  open: boolean;
}) {
  const { data: transactions, isLoading, isError } = useQuery<Transaction[]>({
    queryKey: ["transactions", product.id],
    queryFn: () => getProductTransactions(product.id),
    enabled: open,
  });

  const sortedTransactions = transactions?.sort(
    (a, b) =>
      new Date(b.createdDate!).getTime() - new Date(a.createdDate!).getTime()
  );

  function getTransactionTypeBadge(label: string) {
    switch (label.toLowerCase()) {
      case "compra":
        return { text: "Compra", icon: <ArrowDown className="w-4 h-4 mr-1 text-green-600 dark:text-green-300"/>, bgColor: "bg-green-100 text-green-800 dark:bg-green-800 dark:text-green-100" };
      case "venta":
        return { text: "Venta", icon: <ArrowUp className="w-4 h-4 mr-1 text-blue-600 dark:text-blue-300"/>, bgColor: "bg-blue-100 text-blue-800 dark:bg-blue-800 dark:text-blue-100" };
      case "devolución":
      case "devolucion":
        return { text: "Devolución", icon: <RefreshCcw className="w-4 h-4 mr-1 text-red-600 dark:text-red-300"/>, bgColor: "bg-red-100 text-red-800 dark:bg-red-800 dark:text-red-100" };
      default:
        return { text: label, icon: <CheckCircle className="w-4 h-4 mr-1 text-gray-600 dark:text-gray-300"/>, bgColor: "bg-gray-100 text-gray-800 dark:bg-gray-700 dark:text-gray-200" };
    }
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogContent className="max-w-lg w-full p-6 bg-white dark:bg-gray-800 rounded-xl shadow-xl">
        <DialogHeader className="border-b pb-3 mb-4">
          <DialogTitle className="text-2xl font-bold text-gray-900 dark:text-gray-100">
            {product.name}
          </DialogTitle>
          <DialogDescription className="text-gray-500 dark:text-gray-400">
            Historial de transacciones
          </DialogDescription>
        </DialogHeader>

        <div className="space-y-4">
          <h3 className="text-lg font-semibold text-gray-700 dark:text-gray-200 mb-2">
            Transacciones:
          </h3>

          {isLoading && (
            <p className="text-center text-gray-500 dark:text-gray-400 py-6">Cargando...</p>
          )}

          {isError && (
            <p className="text-center text-red-500 py-6">Error al cargar el historial</p>
          )}

          {!isLoading && !isError && (!sortedTransactions || sortedTransactions.length === 0) && (
            <p className="text-center text-gray-500 dark:text-gray-400 py-6">No existen transacciones</p>
          )}

          {!isLoading && !isError && sortedTransactions && sortedTransactions.length > 0 && (
            <div className="space-y-3 max-h-96 overflow-y-auto pr-2">
              {sortedTransactions.map((t) => {
                const badge = getTransactionTypeBadge(t.transactionLabel);
                const highlightTotal = t.total > 1000; // resaltamos totales altos

                return (
                  <div
                    key={t.id}
                    className="p-4 bg-gray-50 dark:bg-gray-700 rounded-xl shadow-sm hover:shadow-md transition flex flex-col gap-2"
                  >
                    <div className="flex items-center justify-between">
                      <span className={`inline-flex items-center px-3 py-1 rounded-full text-sm font-semibold ${badge.bgColor}`}>
                        {badge.icon} {badge.text}
                      </span>
                      <span className="text-xs text-gray-400 dark:text-gray-300">
                        {t.createdDate ? new Date(t.createdDate).toLocaleDateString() : "-"}
                      </span>
                    </div>

                    <p className="text-gray-800 dark:text-gray-100 font-medium text-sm">
                      Cantidad: <span className="font-bold">{t.quantity}</span>
                    </p>

                    <div className="flex flex-col gap-1 text-xs text-gray-500 dark:text-gray-300">
                      <p>Subtotal: <span className="font-semibold">${t.subtotal}</span></p>
                      <p>Impuesto: <span className="font-semibold">${t.tax}</span></p>
                      <p className={`font-semibold ${highlightTotal ? "text-red-600 dark:text-red-400" : ""}`}>
                        Total: ${t.total}
                      </p>
                    </div>
                  </div>
                );
              })}
            </div>
          )}
        </div>
      </DialogContent>
    </Dialog>
  );
}
