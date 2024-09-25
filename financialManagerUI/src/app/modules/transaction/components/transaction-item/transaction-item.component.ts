import { Component, inject, Input } from '@angular/core';
import { Transaction } from '../../../../entities/models/transaction';
import { formatDate } from '../../utils/date-formatter';
import { ITransactionService } from '../../services/interfaces/transaction.interface';
import { TransactionService } from '../../services/transaction.service';

@Component({
  selector: 'app-transaction-item',
  templateUrl: './transaction-item.component.html',
  styleUrl: './transaction-item.component.css',
})
export class TransactionItemComponent {
  private readonly _transactionService: ITransactionService =
    inject(TransactionService);

  @Input() public transaction: Transaction | undefined;

  public getFormattedDate(date: string | Date): string {
    const formattedDate: string = formatDate(date);
    return formattedDate;
  }

  public deleteTransaction(): void {
    this._transactionService
      .deleteTransaction(this.transaction?.id!)
      .subscribe();
  }
}